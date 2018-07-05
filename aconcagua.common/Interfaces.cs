using System;

namespace aconcagua.common
{
    public interface IDataOperations
    {
        string[][] Get(string[][] operationalData, string[][] seriesData);
        string[][] Set(string[][] operationalData, string[][] seriesData);
    }
}
