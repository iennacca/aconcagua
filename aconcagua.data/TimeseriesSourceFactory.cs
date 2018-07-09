using System.Collections;
using System.Collections.Generic;

namespace aconcagua.data
{
    public class TimeseriesSourceFactory : IReadOnlyDictionary<string, ITimeseriesSource>
    {
        #region Initialization

        public static TimeseriesSourceFactory Factory = new TimeseriesSourceFactory();
        private readonly IDictionary<string, ITimeseriesSource> _sourceDictionary;

        private TimeseriesSourceFactory()
        {
            _sourceDictionary = new Dictionary<string, ITimeseriesSource>();
        }


        // TODO [jc]: refactor the creation algorithm? DI factory for all ITimeseriesSource implementations?
        // TODO [jc]: currently this factory should explicitly know all ITimeseriesSource implementations
        private static ITimeseriesSource CreateSource(string key)
        {
            var sourceKey = new TimeseriesSourceKey(key);

            if (NullTimeseriesSource.TryCreate(sourceKey, out var source))
                return source;

            throw new UnknownTimeseriesSourceException();
        }

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