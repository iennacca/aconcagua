# -*- coding: utf-8 -*-
"""
Created on Sun Jul 29 16:00:26 2018

@author: KMoriya

Test imf_datatools.
"""

import os
import sys
import time

import numpy as np
import pandas as pd

# import imf_datatools to make connections
from imf_datatools import *

# Dict of DataFrames between different sources
dict_dfs = dict()

# Test EcOS
if 's' not in locals():
    s = None # session object
database = 'WEO_WEO_PRODUCTION'
varlist = ['NGDP', 'PPPPC']
countrylist = ['111', '193', '196']

t0 = time.time()
s, df = get_ecos_sdmx_data(database, countrylist, varlist, s=s)
t1 = time.time()
print('Got data from EcOS in ' + "{:.2f}".format(t1-t0) + ' seconds')
dict_dfs['ECOS'] = df

# Close the session since we don't get any more data from EcOS
# s.close()

# Test DMX
# (user will need access to the DMX file)
dmxfilename = r'\\data2\apd\Data\CORE\WORK\apdcore1.DMX'
series = 'A193SP'
t0 = time.time()
df = get_dmx_data(dmxfilename, series)
t1 = time.time()
print('Got data from DMX in ' + "{:.2f}".format(t1-t0) + ' seconds')
dict_dfs['DMX'] = df

# Test World Bank
series = 'NY.GNP.PCAP.CD'
country = 'JAPAN'
t0 = time.time()
df = get_worldbank_data(series, country)
t1 = time.time()
print('Got data from World Bank in ' + "{:.2f}".format(t1-t0) + ' seconds')
dict_dfs['WorldBank'] = df

# Test OECD
database = 'TIVA_2016_C1'
series = 'EXGR_FVA.JPN.WOR.CTOTAL'
t0 = time.time()
df = get_oecd_data(database, series, verify=False)
t1 = time.time()
print('Got data from OECD in ' + "{:.2f}".format(t1-t0) + ' seconds')
dict_dfs['OECD'] = df

# Test SQL server
database = 'PRDDMXSQL'
tablename = 'DMX_WDI'
series = '196.SP.POP.TOTL'
t0 = time.time()
df = get_sql_data(database, tablename, series)
t1 = time.time()
print('Got data from SQL in ' + "{:.2f}".format(t1-t0) + ' seconds')
dict_dfs['SQL'] = df

# Combine all dfs together
df_all = None
for resource in dict_dfs:
    if df_all is None:
        df_all = dict_dfs[resource]
    else:
        df_all = df_all.merge(dict_dfs[resource], how='outer', left_index=True, right_index=True)

print('Created df_all of shape ' + str(df_all.shape))
print(df_all[-10:])


