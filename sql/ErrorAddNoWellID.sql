/*
Author Lalitha Viswanathan
Add an entry to DNAX error table 
*/
INSERT INTO dnax_error
(dnax_error_type, dnax_error_description, dnax_error_timestamp, dnax_operator_id, dnax_hostname)
VALUES
(@errorType, @errorDesc, @timestamp, @operatorID, @hostname)
