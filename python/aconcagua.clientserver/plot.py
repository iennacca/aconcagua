# -*- coding: utf-8 -*-
"""
Created on Mon Aug 27 11:26:03 2018

@author: TWanasukaphun
"""

import pickle
import pandas as pd
import matplotlib.pyplot as plt
    #%%
dat1 = pd.read_pickle('C:\\Users\\TWanasukaphun\\Desktop\\dfts.pkl')
#%% reshaping data 
dat2 = dat1.drop(['source'],axis=1)
dat2 = dat2.set_index('series')
dat3 = dat2.T
dat3 = dat3.reset_index()
#%%
#plotting
dat3.plot(x='index', y=['911BE','911BEA','BCA_GDP'])
dat3.plot(x='index', y='BCA_GDP')

# slicing the data set
dat = dfts

# get series + monthly data
dat = dat[['series'] + [c for c in dat.columns if c[0].isdigit() and 'M' in c]]
tdat = dat.T
tdat.columns = tdat.iloc[0]
tdat = tdat.drop(['series'])
tdat = tdat.reset_index()
tdat.plot(x='index',y=['BCA_GDP'])



