<?php

    $con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');
    
    if (mysqli_connect_errno())
    {
        echo "1: Connection Failed";
        exit();
    }

    $username = $_POST["username"];
    $password = $_POST["password"];
    
    $namecheckquery = "SELECT username FROM players WHERE username = '" . $username ."';";
    
    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Username check failed");
    
    if (mysqli_num_rows($namecheck) > 0)
    {
        echo "3\t";
        exit();
    }
    
    $salt = "\$5\$rounds=5000\$" . "dinosaurdino" . $username . "\$";
    $hash = crypt($password, $salt);
    $insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";
    mysqli_query($con, $insertuserquery) or die("2: Insert user failed");
    
    echo "0\t" . "0";
    
?>