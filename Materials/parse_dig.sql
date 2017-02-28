SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE function [dbo].[parse_dig]
(
    @str varchar(255)
)
returns @ret_table table
(
    id      int identity (1, 1),
    [no]    int
)
as
--
-- slava created this table-valued function at 06-Dec-2005 15:32:16
-- to parse numbers' list into the table
--
-- it is improved version of "_f$RQ2pl_parse_nos2" function
-- "id" field was added to return table for right sorting
--
begin
    set @str = rtrim(ltrim(isnull(@str, '')))
    
    declare @len int
    declare @ptr int
    
    set @len = len(@str)
    set @ptr = 1
    
    if 0 = @len 
        return
    
    declare @s_no varchar(255)
    declare @i_n1 int
    declare @i_n2 int
    declare @b_ar bit
    declare @i    int
    
    set @s_no = ''
    set @i_n1 = 0
    set @i_n2 = 0
    set @b_ar = 0
    
    while (@ptr <= @len)
    begin
        -- skips all not-used characters
        while (@ptr <= @len) and (substring(@str, @ptr, 1) not like '[0-9]')
        begin
            -- checks for the number array sign
            if char(45) = substring(@str, @ptr, 1) and 0 < @i_n2
                set @b_ar = 1
            set @ptr = @ptr + 1
        end
        -- resets locals
        set @s_no = ''
        set @i_n1 = 0
        
        -- gets number
        while (@ptr <= @len) and (substring(@str, @ptr, 1) like '[0-9]')
        begin
            set @s_no = @s_no + substring(@str, @ptr, 1)
            set @ptr = @ptr + 1
        end
        -- checks for number
        set @i_n1 = cast(@s_no as int)
        if 0 < @i_n1
        begin
            --  checks for successive insert mode
            if 0 = @b_ar
            begin
                insert into @ret_table select @i_n1
                set @i_n2 = @i_n1
            end
            else if @i_n1 < @i_n2
            begin
                set @i = @i_n1
                while @i <= @i_n2 - 1
                begin
                    insert into @ret_table select @i
                    set @i = @i + 1
                end
                set @b_ar = 0
                set @i_n2 = 0
            end
            else if @i_n1 >=@i_n2
            begin
                set @i = @i_n2 + 1
                while @i <= @i_n1
                begin
                    insert into @ret_table select @i
                    set @i = @i + 1
                end
                set @b_ar = 0
                set @i_n2 = 0
            end     
        end
    end
    
    return
end




GO


