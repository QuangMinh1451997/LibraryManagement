use QLThuVien

create trigger after_books_update_insert
ON Books
FOR UPDATE, INSERT
AS
BEGIN

	--update
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
	BEGIN
		DECLARE @oldBookTypeID int, @newBookTypeID int
		Select @oldBookTypeID = BookTypeID from deleted
		select @newBookTypeID = BookTypeID from inserted

		UPDATE BookTypes SET Quantity = (Select COUNT(BookID) from Books 
			Where BookTypeID=@oldBookTypeID and Deleted=0) Where BookTypeID=@oldBookTypeID
		IF @oldBookTypeID != @newBookTypeID
		BEGIN
			UPDATE BookTypes SET Quantity = (Select COUNT(BookID) from Books 
				Where BookTypeID=@newBookTypeID and Deleted=0) Where BookTypeID=@newBookTypeID
		END
	END

	-- insert
	IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS(SELECT * FROM deleted)
	BEGIN
		DECLARE @bookTypeID int

		select @bookTypeID = BookTypeID from inserted

		UPDATE BookTypes SET Quantity = (Select COUNT(BookID) from Books 
			Where BookTypeID=@bookTypeID and Deleted=0) Where BookTypeID=@bookTypeID
	END
END