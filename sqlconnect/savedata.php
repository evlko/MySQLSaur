<?php

    $con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');
    
    if (mysqli_connect_errno())
    {
        echo "1: Connection Failed";
        exit();
    }
 
    $username = $_POST["username"];
    $newhighscore = $_POST["highscore"];
       
    $namecheckquery = "SELECT username FROM players WHERE username = '" . $username ."';";
   
    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Username check failed");
   
    if (mysqli_num_rows($namecheck) != 1)
    {
        echo "5: No user with this name OR several exist";
        exit();
    }
    
    $updatequery = "UPDATE players SET highscore = " . $newhighscore . " WHERE username = '$username';";
    mysqli_query($con, $updatequery) or die("7: Save query failed");
    
    echo("0");
    
?>