update [MDW].[dbo].[TMP]
set F1 = RTRIM(ltrim(F1))

SELECT [F1], '[' + 
		SUBSTRING(F1, charindex(' ', F1), LEN(F1) - charindex(' ', F1)) + ', ' +
		SUBSTRING(F1, 0, charindex(' ', F1)) + '],'
  FROM [MDW].[dbo].[TMP]