/*
Add entry to DNAX error (DNA Extraction Module)
*/
INSERT INTO dnax_error
(dnax_error_type, dnax_error_description, dnax_error_timestamp, well_id, dnax_operator_id, dnax_hostname)
VALUES
(@errorType, @errorDesc, @timestamp, @wellID, @operatorID, @hostname)
