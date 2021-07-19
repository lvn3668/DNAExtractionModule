/* Author Lalitha Viswanathan
Add Deep Well plate information to DB 
*/
INSERT INTO deepwell
 (plate_id, date_transferred, volume, robot_name, script_name, dnax_operator_id, dnax_timestamp, dnax_hostname)
VALUES
(@plateID, @dateTransferred,@vol, @robotName, @scriptName, @dnaxOperatorID, @dnaxTimestamp, @dnaxHostname)