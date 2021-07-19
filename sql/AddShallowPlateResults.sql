/*
Add shallow plate records plate id, aliquot id, plate column and row 
*/
INSERT INTO shallowplate_well
 (plate_id, aliquot_id, plate_column,plate_row)
VALUES
(@plateID, @aliquotID,@plateColumn,@plateRow)