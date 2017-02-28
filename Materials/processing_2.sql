delete FROM [MDW].[dbo].[sc_ParseStreet]

DECLARE @cursor cursor


SET @cursor = CURSOR FOR
SELECT [F2]
      ,[F3]    
      ,[NUMBERS]
  FROM [MDW].[dbo].[sc_AREA]
  where numbers is not null


declare @num	varchar(255)
declare @street	varchar(255)
declare @nums	varchar(255)
declare @num_parse	varchar(255)
declare @pos	int

OPEN @cursor
FETCH NEXT FROM @cursor
	INTO @num, @street, @nums


WHILE 0 = @@fetch_status
BEGIN

	DECLARE @cur cursor
	SET @cur = CURSOR FOR
		SELECT replace(ltrim(rtrim(word)), char(9), '') FROM [MDW].[dbo].[split_str] ( @nums, ',')	

	OPEN @cur
	FETCH NEXT FROM @cur
		INTO @num_parse

	WHILE 0 = @@fetch_status
	BEGIN

        SET @pos = CHARINDEX('-', @num_parse, 0)
        IF @pos = 0
        BEGIN
			insert into [dbo].[sc_ParseStreet] (NUM, Street, Number) values (@num, @street, ltrim(rtrim(@num_parse)))
		END
		ELSE
		BEGIN
			insert into [dbo].[sc_ParseStreet] (NUM, Street, Number) 
			select @num, @street, ltrim(rtrim(cast(no as varchar(50)))) FROM [MDW].[dbo].[parse_dig] (@num_parse)
		END

		FETCH NEXT FROM @cur
		INTO @num_parse
	END

	DEALLOCATE @cur


	FETCH NEXT FROM @cursor
	INTO @num, @street, @nums

END

update [MDW].[dbo].[sc_ParseStreet] set Number = REPLACE(Number, '  ', ' ')
update [MDW].[dbo].[sc_ParseStreet] set Number = REPLACE(Number, '  ', ' ')
update [MDW].[dbo].[sc_ParseStreet] set Number = REPLACE(Number, '  ', ' ')
update [MDW].[dbo].[sc_ParseStreet] set Number = REPLACE(Number, '  ', ' ')

