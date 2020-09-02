xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
where $publication/p1:authors/p1:author/@username = "%s" and $publication/p1:header/p1:status != "withdrawn"
return $publication

return <publications> {$publications} </publications>