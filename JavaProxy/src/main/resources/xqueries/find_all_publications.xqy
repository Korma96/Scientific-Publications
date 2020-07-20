xquery version "3.1";

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/publication
where $publication/header/status != "deleted"
return $publication

return <publications> {$publications} </publications>