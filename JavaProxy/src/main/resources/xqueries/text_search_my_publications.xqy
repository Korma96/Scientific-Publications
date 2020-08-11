xquery version "3.1";

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/publication
where $publication/authors/author/@username = "%s" and
         (matches($publication/title, "%s","i") or
          matches($publication/authors/author/name, "%<s", "i") or
          matches($publication/authors/author/university, "%<s","i") or
          matches($publication/abstract/purpose, "%<s", "i") or
          matches($publication/abstract/problem, "%<s", "i") or
          matches($publication/abstract/method, "%<s", "i") or
          matches($publication/abstract/results, "%<s", "i") or
          matches($publication/abstract/conclusions, "%<s", "i") or
          matches($publication/keywords, "%<s", "i") or
          matches($publication/section/heading, "%<s", "i") or
          matches($publication/section/content/text, "%<s", "i") or
          matches($publication/section/content/imageContent/about, "%<s", "i") or
          matches($publication/section/subsection/content/text, "%<s", "i") or
          matches($publication/section/subsection/content/imageContent/about, "%<s", "i") or
          matches($publication/bibliography/reference, "%<s", "i"))

return $publication


return <publications> &#xa; {$publications} &#xa;</publications>