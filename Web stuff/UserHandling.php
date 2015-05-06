<?php
include_once("php.php");


$user_id = 0;
$result = mysql_query("SELECT MAX(UserID) as LastUser FROM AwkwardDate_Users");
$user_id = intval(mysql_result($result,0,"LastUser")) + 1;
$color = random_color();
$time = time();
mysql_query("INSERT INTO AwkwardDate_Users (UserID, Color, LastSeen) VALUES ('$user_id', '$color', '$time')");


function random_color() {
    return random_color_part() . random_color_part() . random_color_part();
}

function random_color_part() {
    return str_pad( dechex( mt_rand( 0, 255 ) ), 2, '0', STR_PAD_LEFT);
}

echo $user_id;
?>