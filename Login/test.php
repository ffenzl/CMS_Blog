<?php
	ob_start();
	session_start(); 

	$host="localhost"; // Host name 
	$username="root"; // Mysql username 
	$password=""; // Mysql password 
	$db_name="cms_blog"; // Database name 
	$tbl_name="user"; // Table name

	// Connect to server and select databse.
	$connnection = mysqli_connect("$host", "$username", "$password");
	
	if (mysqli_connect_errno())
	{
		
	}
	else 
	{
	}
	
	mysqli_select_db($connnection, "$db_name");
	// Check $username and $password 

	if(empty($_POST['usermail']))
	{
		return false;
	}
	if(empty($_POST['password']))
	{
		return false;
	}


	// Define $username and $password 
	$username=$_POST['usermail']; 
	$password=md5($_POST['password']);


	// To protect MySQL injection (more detail about MySQL injection)
	$username = stripslashes($username);
	$password = stripslashes($password);
	$password = crypt($password, "$6$498484");

	$sql="SELECT * FROM user WHERE user_email='$username' and user_password='$password'";
	
	$result=mysqli_query($connnection, $sql);

	// Mysql_num_row is counting table row
	$count=mysqli_num_rows($result);
	// If result matched $username and $password, table row must be 1 row
	if ($count==1) {
		$_SESSION["login"] = 1; 
		include("login.php"); 
	} else {
		include("index.php"); 
	}

	ob_end_flush();
?>