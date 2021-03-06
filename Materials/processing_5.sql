/****** Script for SelectTopNRows command from SSMS  ******/
SELECT	
		'{ street: "' + 
		substring([address], 0, charindex(',', [address]))
		+ '", buildNum: "' + 
		ltrim(rtrim(substring([address], charindex(',', [address]) + 1, LEN([address]) - charindex(',', [address]))))
		+ '", lattitude:"' + 
		convert(varchar(32), cast([lat6] as decimal(38,12)))
		+ '", longtitude: "' + 
		convert(varchar(32), cast([lon5] as decimal(38,12)))
		+ '",  school: "' + 
		[name7]
		+ '"}'
  FROM [MDW].[dbo].[sc_schools]
order by cast(name7 as int)


select * FROM [MDW].[dbo].[sc_schools]
order by lon5, lat6

