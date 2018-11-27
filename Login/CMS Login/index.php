<!doctype html>
<html lang="de">
  <head>
	<!-- Bootstrap themes -->
	<link rel="stylesheet" type="text/css" href="css_bootstrap/bootstrap.min.css" />

	<!-- Dashboard -->
  <link href="css/signin.css" rel="stylesheet">

  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <link rel="icon" href="images/favicon.png">

  <title>CMS Blog Login</title>
</head>

<body>

  <form class="form-signin" action="test.php" method="POST">
    <img class="mb-4" src="images/favicon.png" alt="" height="72">
    <h1 class="h3 mb-3 font-weight-normal">Bitte einloggen</h1>
    <label for="inputEmail" class="sr-only">Email Adresse</label>
    <input type="email" id="usermail" name="usermail" class="form-control" placeholder="Email Adresse" required autofocus>
    <label for="inputPassword" class="sr-only">Passwort</label>
    <input type="password" id="password" name="password" class="form-control" placeholder="Passwort" required>
    <button class="btn btn-lg btn-primary btn-block" type="submit">Einloggen</button>
    <p class="mt-5 mb-3 text-muted">&copy; 2018</p>
  </form>

  <!-- Bootstrap core JavaScript
  ================================================== -->
  <!-- Jquery -->
	<script type="text/javascript" src="js_jquery/jquery-3.3.1.min.js"></script>
	<!-- Bootstrap library -->
	<script type="text/javascript" src="js_bootstrap/bootstrap.min.js"></script>
  <!-- Eigenes Javascript -->
  <!-- ================================================== -->
  </body>
</html>
