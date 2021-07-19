/*
Author Lalitha Viswanathan 
Decrement aliquot volumes
*/
UPDATE aliquot 
SET 
current_amount = @volume
WHERE
aliquot_id = @aliquotID