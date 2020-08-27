xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
     where $publication/p1:bibliography/p1:reference/@p1:refId = "%s"
     return $publication

return <publications> {$publications} </publications>