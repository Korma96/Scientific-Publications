xquery version "3.1";

for $publication in fn:doc("/db/test/publications.xml")/publications/publication
where $publication/@id = "%s"
return  if (%b())
then update value $publication/header/status with "accepted"
else update value $publication/header/status with "rejected"