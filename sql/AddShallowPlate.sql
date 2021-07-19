/*
Add shallow plate to DB (plate id, extracted date, volume, robot, script (from extraction machine), operator id, timestamp)
*/
INSERT INTO shallowplate
 (plate_id, date_extracted, volume, robot_name, script_name, dnax_operator_id, dnax_timestamp, dnax_hostname)
VALUES
(@plateID, @dateExtracted,@vol, @robotName, @scriptName, @dnaxOperatorID, @dnaxTimestamp, @dnaxHostname)