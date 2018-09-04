using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Xml.Linq;
using aconcagua.data.ecos.SdmxServiceReference;
using System.Collections;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace aconcagua.data.ecos
{
    public class ECOSTimeseriesSource : ITimeseriesSource, IDisposable
    {
        private const string _schemeType = "ecos";
        private static SDMXServiceClient _client;
        private static readonly XNamespace Progg = @"http://www.SDMX.org/resources/SDMXML/schemas/v2_0/generic";
        private static readonly XNamespace Progm = @"http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message";

        private static string DatabaseID;
        public string SchemeType { get; } = _schemeType;
        public TimeseriesSourceKey SourceKey { get; }


        public ECOSTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;
            DatabaseID = sourceKey.Key.LocalPath;
        }

        public static bool TryCreate(TimeseriesSourceKey sourceKey, out ITimeseriesSource timeseriesSource)
        {
            timeseriesSource = null;
            if (String.Equals(sourceKey.Key.Scheme, _schemeType))
                timeseriesSource = new ECOSTimeseriesSource(sourceKey);
            return (timeseriesSource != null);
        }

        public IQueryable<ITimeseriesObservations> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, string frequencies)
        {
            var seriesList = new Dictionary<TimeseriesKey, ITimeseriesObservations>();

            _client = new SDMXServiceClient();


            var genericDataSet = _client.GetGenericData(GetObservationsQuery.BuildObservationQuery(DatabaseID, seriesKeys.ToTimeseriesKeyString(QuotedSplitStringOptions.Quoted)));

            var dataSet = genericDataSet.Element(Progm + "DataSet");
            if (dataSet != null)
            {
                var seriesElements = dataSet.Elements(Progg + "Series");
                foreach (var seriesElement in seriesElements)
                {
                   // string seriesKey1 = ((TimeseriesKey[])seriesKeys)[0].Key;
                    var seriesKey = new TimeseriesKey(((TimeseriesKey[])seriesKeys)[0].Key);
                    var seriesAttributes = seriesElement.Element(Progg + "Attributes");
                    var seriesObservation = seriesElement.Elements(Progg + "Obs");
                    ITimeseriesObservations series;
                    if (!seriesList.ContainsKey(seriesKey))
                    {
                        series = new EcOSTimeseries(SourceKey, seriesKey);
                        seriesList.Add(seriesKey, series);
                    }
                    else
                        series = seriesList[seriesKey];


                    foreach (var observation in seriesObservation)
                    {
                        var time = observation.Element(Progg + "Time");
                        var obsEl = observation.Element(Progg + "ObsValue");
                        if (obsEl != null && time != null)
                        {
                            var obs = obsEl.Attribute("value");
                            if (obs != null) series.Observations[time.Value] = Convert.ToDouble(obs.Value);
                            ;
                        }
                    }


                }
            }

            return seriesList.Values.AsQueryable();

        }

        public IQueryable<ITimeseriesMetadata> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
           
            var seriesList = new List<ITimeseriesMetadata>();


            _client = new SDMXServiceClient();

            var genericMetadata = _client.GetTimeSeriesAttributes(GetMetadataQuery.BuildMetadataQuery(DatabaseID, seriesKeys.ToTimeseriesKeyString(QuotedSplitStringOptions.Quoted)));

            var metadataSet = genericMetadata.Element(Progm + "DataSet");

            if (metadataSet != null)
            {
                var seriesElements = metadataSet.Elements(Progg + "Series");
                foreach (var seriesElement in seriesElements)
                {

                    var seriesAttr = seriesElement.Element(Progg + "SeriesKey");
                    var seriesAttributes = seriesElement.Element(Progg + "Attributes");
                    var metadataDictionary = new Dictionary<string, string>();
                    //string seriesKey = ((TimeseriesKey[])seriesKeys)[0].Key;
                    var seriesKey = new TimeseriesKey(((TimeseriesKey[])seriesKeys)[0].Key);

                    foreach (var value in seriesAttr.Elements())
                    {

                        var conceptAtt = value.Attribute("concept");
                        var valueAtt = value.Attribute("value");
                        if (headerList.Any(str => str.Contains(conceptAtt.Value)))
                        {
                            metadataDictionary[conceptAtt.Value] = valueAtt.Value;
                        }

                    }

                    foreach (var value in seriesAttributes.Elements())
                    {
                        var conceptAtt = value.Attribute("concept");
                        var valueAtt = value.Attribute("value");
                        if (headerList.Any(str => str.Contains(conceptAtt.Value)))
                        {
                            metadataDictionary[conceptAtt.Value] = valueAtt.Value;
                        }
                    }

                    seriesList.Add(new EcOSTimeseries(SourceKey, seriesKey, metadataDictionary));


                }
            }

            return seriesList.AsQueryable();
        }


        #region IDisposable

        private readonly bool _disposed = false;
        private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                //  _connection.Close();
                _handle.Dispose();
            }
        }

        #endregion


        private class EcOSTimeseries : ITimeseriesMetadata, ITimeseriesObservations
        {
            public TimeseriesSourceKey SourceKey { get; }
            public TimeseriesKey SeriesKey { get; }
            public IDictionary<string, string> Metadata => _metadata;
            public IDictionary<string, double> Observations => _observations;

            private readonly Dictionary<string, string> _metadata = new Dictionary<string, string>();
            private readonly Dictionary<string, double> _observations = new Dictionary<string, double>();

            internal EcOSTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey)
            {
                SourceKey = sourceKey;
                SeriesKey = seriesKey;
            }

            internal EcOSTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, Dictionary<string, string> metadata) : this(sourceKey, seriesKey)
            {
                _metadata = metadata;
            }

            internal EcOSTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, Dictionary<string, double> observations) : this(sourceKey, seriesKey)
            {
                _observations = observations;
            }
        }

    }


    internal static class GetObservationsQuery
    {

        public static XElement BuildObservationQuery(string dataSetId, string seriescode)
        {

            var attributeList = new StringBuilder();

            attributeList.AppendLine("<Or>");
            foreach (var code in seriescode.Split(','))
            {
                string attribute = @"<Attribute id=""SERIESCODE"">{0}</Attribute>";
                attribute = string.Format(attribute, seriescode.Replace("'", ""));
                attributeList.AppendLine(attribute);
            }

            attributeList.AppendLine("</Or>");


            var queryText =
                String.Format(
                    @"<QueryMessage xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message"">
                        <Query>
                            <DataWhere xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/query"">
                                <And>
                                    <DataSet>{0}</DataSet>
                                    {1}
                                </And>
                            </DataWhere>
                        </Query>
                    </QueryMessage>",
                    dataSetId, attributeList);

            return XElement.Parse(queryText);
        }

    }


    internal static class GetMetadataQuery
    {


        public static XElement BuildMetadataQuery(string dataSetId, string seriesCode)
        {
            var attributeList = new StringBuilder();

            attributeList.AppendLine("<Or>");
            foreach (var code in seriesCode.Split(','))
            {
                string attribute = @"<Attribute id=""SERIESCODE"">{0}</Attribute>";
                attribute = string.Format(attribute, seriesCode.Replace("'", ""));
                attributeList.AppendLine(attribute);
            }

            attributeList.AppendLine("</Or>");


            var queryText =
                String.Format(
                    @"<QueryMessage xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message"">
                        <Query>
                            <DataWhere xmlns=""http://www.SDMX.org/resources/SDMXML/schemas/v2_0/query"">
                                <And>
                                    <DataSet>{0}</DataSet>
                                    {1}
                                </And>
                            </DataWhere>
                        </Query>
                    </QueryMessage>",
                    dataSetId, attributeList);

            return XElement.Parse(queryText);
        }
    }


}
