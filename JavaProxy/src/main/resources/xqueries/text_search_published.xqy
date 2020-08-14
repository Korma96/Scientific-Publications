xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
where $publication/p1:header/p1:status = "accepted" and
         (matches($publication/p1:title, "%s","i") or
          matches($publication/p1:authors/p1:author/p1:name, "%<s", "i") or
          matches($publication/p1:authors/p1:author/p1:university, "%<s","i") or
          matches($publication/p1:abstract/p1:purpose, "%<s", "i") or
          matches($publication/p1:abstract/p1:problem, "%<s", "i") or
          matches($publication/p1:abstract/p1:method, "%<s", "i") or
          matches($publication/p1:abstract/p1:results, "%<s", "i") or
          matches($publication/p1:abstract/p1:conclusions, "%<s", "i") or
          matches($publication/p1:keywords, "%<s", "i") or
          matches($publication/p1:section/p1:heading, "%<s", "i") or
          matches($publication/p1:section/p1:content/p1:text, "%<s", "i") or
          matches($publication/p1:section/p1:content/p1:imageContent/p1:about, "%<s", "i") or
          matches($publication/p1:section/p1:subsection/p1:content/p1:text, "%<s", "i") or
          matches($publication/p1:section/p1:subsection/p1:content/p1:imageContent/p1:about, "%<s", "i") or
          matches($publication/p1:bibliography/p1:reference, "%<s", "i"))

return $publication


return <publications> &#xa; {$publications} &#xa;</publications>