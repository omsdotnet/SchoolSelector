SELECT distinct
	   [F1]
      ,[F2]
      , sa.*
  FROM [MDW].[dbo].[sc_AREA] as arr
	left join [dbo].[sc_data_all] as sa on sa.[название] = arr.F2
  where F2 is not null
  order by F1, F2