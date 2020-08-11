xquery version "3.1";

for $publication in fn:doc("/db/test/publications.xml")/publications/publication
where $publication/@id = "%s" and $publication/header/status != "published"
return update value $publication/header/status with "deleted"