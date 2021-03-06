update [MDW].[dbo].[sc_AREA]
	set F3 = null
where F3 = ''
go	


DECLARE @cursor cursor
declare @id		int
declare @num	varchar(255)
declare @street varchar(255)
declare @nums	varchar(255)

declare @num_prev		varchar(255)
declare @street_prev	varchar(255)
declare @mums_prev		varchar(255)

declare @numbers		varchar(max)
declare @num_lock		varchar(255)
declare @id_lock		int
declare @num_lock_prev		varchar(255)
declare @id_lock_prev		int


SET @cursor = CURSOR FOR
SELECT ID, F2, F3, F4
  FROM [MDW].[dbo].[sc_AREA]
  order by id
  

set @numbers = ''
  
OPEN @cursor
FETCH NEXT FROM @cursor
	INTO @id, @num, @street, @nums

set @num_prev =		@num
set @street_prev =	@street
set @mums_prev =	@nums

set @num_lock_prev =		@num
set @id_lock_prev =		@id
set @num_lock =		@num
set @id_lock =		@id

WHILE 0 = @@fetch_status
BEGIN
	if (@num is not null) 
	begin
		set @num_lock_prev	= @num_lock
		set @num_lock	= @num
	end
	
	if (@street is not null)
	begin
		set @id_lock_prev	= @id_lock
		set @id_lock	= @id	
	end


	if ((@street is not null) and (@street_prev is null)) or
	   ((@street is not null) and (@street_prev is not null) and (@street <> @street_prev))
	begin
		update sc_AREA
			set F2 = @num_lock
		where ID = @id
		
		update sc_AREA
			set NUMBERS = @numbers
		where ID = @id_lock_prev
		
		set @numbers = @nums
	end
	else
	begin
		set @numbers = @numbers + @nums	
	end



	set @num_prev =		@num
	set @street_prev =	@street
	set @mums_prev =	@nums

	FETCH NEXT FROM @cursor
	INTO @id, @num, @street, @nums

END

update sc_AREA
	set NUMBERS = @numbers
where ID = @id_lock
go
