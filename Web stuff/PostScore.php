<?php
include_once("php.php");
if(!empty($_GET))
{
    $user_id = mysql_real_escape_string($_GET['id'], $database);
    $inputs_as_json = mysql_real_escape_string($_GET["inputs"], $database);
    $input_array = json_decode($inputs_as_json);
    $time = time();
    for($i = 0; $i < count($input_array); $i++)
    {

        $moveX = $input_array[$i][0];
        $moveY = $input_array[$i][1];
        echo $moveX . ", " . $moveY . " ... ";
        mysql_query("INSERT INTO AwkwardDate_UserInput (UserID, MoveX, MoveY) VALUES ('$user_id','$moveX','$moveY')");


    }
    mysql_query("UPDATE AwkwardDate_Users SET LastSeen='$time' WHERE UserID='$user_id'");
    echo "user: $user_id sent " . count($input_array) . " data points";
}

?>