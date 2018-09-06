# -*- coding: utf-8 -*-
"""
Created on Mon Aug 27 11:26:03 2018

@author: TWanasukaphun
"""

import pickle
import pandas as pd
import matplotlib.pyplot as plt

##%%
#pdfts1 = pd.read_pickle('C:\\Users\\TWanasukaphun\\Desktop\\dfts.pkl')
##%% reshaping dfts 
#pdfts2 = pdfts1.drop(['source'],axis=1)
#pdfts2 = pdfts2.set_index('series')
#pdfts3 = pdfts2.T
#pdfts3 = pdfts3.reset_index()
##%%
#plotting
#pdfts3.plot(x='index', y=['911BE','911BEA','BCA_GDP'])
#pdfts3.plot(x='index', y='BCA_GDP')


# getting data set from pickle --------------------------------
df = pd.read_pickle('c:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.pkl')

# get list of series
s = df['series'].tolist()

# get series + annual data
pdf = df[['series'] + [c for c in df.columns if c[0].isdigit() and 'A' in c]]

# reshape data
pdf = pdf.T
pdf = pdf.reset_index()

#change the column names
pdf.columns = ['series'] + df['series'].tolist()
pdf.rename(index=str, columns={"series": "label"})

#drop the series row
pdf = pdf.drop(pdf.index[pdf['label'] == 'series'])

pdf.plot(y=s)
plt.show()


