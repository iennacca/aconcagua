using System.Collections;
using System.Collections.Generic;
using aconcagua.data.dmx;

namespace aconcagua.data.factory
{
    public class TimeseriesSourceFactory : IReadOnlyDictionary<string, ITimeseriesSource>
    {
        #region Initialization

        public static TimeseriesSourceFactory Factory = new TimeseriesSourceFactory();
        private readonly IDictionary<string, ITimeseriesSource> _sourceDictionary;

        private TimeseriesSourceFactory()
        {
            _sourceDictionary = new Dictionary<string, ITimeseriesSource>();
            // _timeseriesSourceTypes = getITimeseriesSourceImplementations();
        }

        #endregion

        private static ITimeseriesSource CreateSource(string key)
        {
            var sourceKey = new TimeseriesSourceKey(key);
            ITimeseriesSource source;

            // loop through all ITimeseriesSource implementations here
            if (NullTimeseriesSource.TryCreate(sourceKey, out source))
                return source;

            if (DMXTimeseriesSource.TryCreate(sourceKey, out source))
                return source;

            throw new UnknownTimeseriesSourceException();
        }

        #region ITimeseriesSource.TryCreate cross-assembly check

        //// TODO [jc]: refactor the creation algorithm? DI factory for all ITimeseriesSource implementations?
        //// TODO [jc]: currently this factory iterates through all known ITimeseriesSource implementations in assembly
        //private static IEnumerable<Type> _timeseriesSourceTypes;

        //private IEnumerable<Type> getITimeseriesSourceImplementations()
        //{
        //    // var tssTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITimeseriesSource)));

        //    var tssAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        //    var tssTypes = new List<Type>();

        //    foreach (var tssAssembly in tssAssemblies)
        //        tssTypes.AddRange(tssAssembly.GetTypes()
        //            .Where(t => t.GetInterfaces().Contains(typeof(ITimeseriesSource))));
        //    return tssTypes;
        //}

        //private static ITimeseriesSource CreateSource(string key)
        //{
        //    var sourceKey = new TimeseriesSourceKey(key);

        //    // loop through all ITimeseriesSource implementations here
        //    foreach (var typeTimeseriesSource in _timeseriesSourceTypes)
        //    {
        //        //if (typeTimeseriesSource.TryCreate(sourceKey, out var source))
        //        //    return source;
        //        var source = TryCreateSource(typeTimeseriesSource, sourceKey);
        //        if (source != null) return source;
        //    }

        //    throw new UnknownTimeseriesSourceException();
        //}

        //private static ITimeseriesSource TryCreateSource(Type tssType, TimeseriesSourceKey sourceKey)
        //{
        //    // let GetMethod() throw exception if not implemented
        //    var methodInfo = tssType.GetMethod("TryCreate");
        //    var parameters = new object[] {sourceKey, null};

        //    methodInfo.Invoke(null, parameters);
        //    return (ITimeseriesSource) parameters[1];
        //}
        #endregion

        #region IReadOnlyDictionary implementation

        public IEnumerator<KeyValuePair<string, ITimeseriesSource>> GetEnumerator()
        {
            return _sourceDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_sourceDictionary).GetEnumerator();
        }

        public int Count => _sourceDictionary.Count;

        public bool ContainsKey(string key)
        {
            return _sourceDictionary.ContainsKey(key);
        }

        public bool TryGetValue(string key, out ITimeseriesSource value)
        {
            return _sourceDictionary.TryGetValue(key, out value);
        }

        public ITimeseriesSource this[string keyUri]
        {
            get
            {
                if (!_sourceDictionary.ContainsKey(keyUri))
                    _sourceDictionary.Add(keyUri, CreateSource(keyUri));
                return _sourceDictionary[keyUri];
            }
        }

        public IEnumerable<string> Keys => _sourceDictionary.Keys;
        public IEnumerable<ITimeseriesSource> Values => _sourceDictionary.Values;

        #endregion
    }
}