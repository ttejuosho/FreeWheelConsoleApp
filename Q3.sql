Declare @MARKET_ID int
Declare @CELL_ID int
Declare @CELL_RECORD_COUNT BIGINT = 0

Select @CELL_RECORD_COUNT = COUNT(0) from CELL

while @CELL_ID <= @CELL_RECORD_COUNT
Select @MARKET_ID = MARKET_ID from MARKET_POP where MARKET_ID = @MARKET_ID and CELL_ID = @CELL_ID

IF(@MARKET_ID is null)
Begin
    Insert into MARKET_POP values (@MARKET_ID, @CELL_ID)
End
