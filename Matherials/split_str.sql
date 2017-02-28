SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE  FUNCTION [dbo].[split_str]
(
	@content	varchar(1500),
	@splitter	varchar(50)
)
RETURNS	@return_table TABLE
(
	word 		varchar(512),
	id			int identity (1,1)
)
AS
--
-- pavel created this function for split strings
--
BEGIN

    declare @length1 int
    declare @length2 int

	declare @i      int
    declare @p      int

    declare @word     varchar(512)
    declare @char     varchar(50)
  
    set @length1 = len(@content)
	set @length2 = len(@splitter + '.') - 1

	if (@length2 < @length1) and (@length2 >0 )
	begin
    	set @i = 1
    	set @p = @i

     	while (@i <= @length1)
        begin
            set @char = substring(@content, @i, @length2)

            if (@char = @splitter)
            begin
				set @word = substring(@content, @p, @i - @p)                
				insert into @return_table (word) values (@word)				
                set @i = @i + @length2
				set @p = @i
            end
			else
				set @i = @i + 1				
        end

		if (@i > @p)
		begin
			set @word = substring(@content, @p, @i - @p)                
			insert into @return_table (word) values (@word)
		end

    end

--select * from @return_table

    RETURN

END

-- select * from [dbo].[split_str] ('привет это проверка 1-9', ' ')




GO


