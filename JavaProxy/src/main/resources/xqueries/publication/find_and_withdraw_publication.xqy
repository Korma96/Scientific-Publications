xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";


for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
where $publication/@p1:id = "%s" and $publication/p1:header/p1:status != "accepted"
return update value $publication/p1:header/p1:status with "withdrawn"