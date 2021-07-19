/*
Adds deep well plate, aliquot, row and column to database
*/
INSERT INTO deepwell_well
 (plate_id, aliquot_id, plate_column,plate_row)
VALUES
(@plateID, @aliquotID,@plateColumn,@plateRow)