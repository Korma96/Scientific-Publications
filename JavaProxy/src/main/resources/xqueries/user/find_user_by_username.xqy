xquery version "3.1";

for $user in fn:doc("/db/test/users.xml")/users/user
where $user/username = "%s"
return $user