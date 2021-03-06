update [MDW].[dbo].[sc_data_all]
 set [название] = substring([название], CHARINDEX('№', [название]) +1, LEN([название]) - CHARINDEX('№', [название])) 

update [MDW].[dbo].[sc_data_all]
set [название] = case 
					when CHARINDEX(' ', [название]) > 0 then substring([название], 1, CHARINDEX(' ', [название])) 
					else [название]
				 end

update [MDW].[dbo].[sc_data_all]
	set [название] = CAST([название] as int)



SELECT 
	'{ school: ' +
	[название]
	+ ', egCommonRating: "' +
	cast([средний балл] as varchar(16))
	+ '", egCommomLevel: "' + 
	cast([место] as varchar(16))
	+ '", egCommonState: "' + 
	case [уровень]
		when  2 then 'Самый высокий'
		when  1 then 'Выше среднего'
		when  0 then 'Средний'
		when  -1 then 'Ниже среднего'
		when  -2 then 'Самый низкий'
	end
	+ '" }'
from [dbo].[sc_data_all]


 
