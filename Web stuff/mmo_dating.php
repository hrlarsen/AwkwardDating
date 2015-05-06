
<!doctype html>
<html lang=en>
<head>
    <meta charset=utf-8>
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <style type="text/css">

        * {
            -webkit-touch-callout: none; /* prevent callout to copy image, etc when tap to hold */
            -webkit-text-size-adjust: none; /* prevent webkit from resizing text to fit */
            /* make transparent link selection, adjust last value opacity 0 to 1.0 */
            -webkit-tap-highlight-color: rgba(0,0,0,0);
            -webkit-user-select: none; /* prevent copy paste, to allow, change 'none' to 'text' */
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

        body {
            background-color: #000000;
            margin: 0px;
            color: white;
        }
        canvas {
            display:block;
            position:absolute;
        }
        .container {
            width:auto;
            text-align:center;
            background-color:#ff0000;
        }
    </style>


</head>
<body onload = "init()">
<div id="info">
    Touch device: Steer on left side of the screen, interact on the right. Keyboard arrowkeys + L
    <br>
</div>


<script src="js/Vector2.js"></script>
<script src="js/ShipMovingTouch.js"></script>
<script src="js/BulletSebs.js"></script>
<script src="THREEx.KeyboardState.js"></script>
<script type="text/javascript">

    var canvas,
            c, // c is the canvas' context 2D
            container,
            halfWidth,
            halfHeight,
            leftTouchID = -1,
            leftTouchPos = new Vector2(0,0),
            leftTouchStartPos = new Vector2(0,0),
            leftVector = new Vector2(0,0);

    var vector2zero = new Vector2(0,0);
    var keyboard = new THREEx.KeyboardState();

    setupCanvas();

    var mouseX, mouseY,
    // is this running in a touch capable environment?
            touchable = 'createTouch' in document,
            touches = [], // array of touch vectors
            ship = new ShipMoving(halfWidth, halfHeight)
            bullets = [],
            spareBullets = [];

    var timeSinceLastAction = 0;

    document.body.appendChild(ship.canvas);

    var framerate = 30;
    var deltaTime = 1/framerate * 1000;
    var postTimeTracker = 0;
    var postRate = 0.1 * 1000; // miliseconds

    setInterval(draw, deltaTime);

    var inputBuffer = [];
    var tickBuffer = 0;
    var userID = 0;

    if(touchable) {
        canvas.addEventListener( 'touchstart', onTouchStart, false );
        canvas.addEventListener( 'touchmove', onTouchMove, false );
        canvas.addEventListener( 'touchend', onTouchEnd, false );
        window.onorientationchange = resetCanvas;
        window.onresize = resetCanvas;
    } else {

        canvas.addEventListener( 'mousemove', onMouseMove, false );
    }

    function resetCanvas (e) {
        // resize the canvas - but remember - this clears the canvas too.
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;

        halfWidth = canvas.width/2;
        halfHeight = canvas.height/2;

        //make sure we scroll to the top left.
        window.scrollTo(0,0);
    }

    var canShoot = true;

    function init(){
        GetUserID();
        keyboard.domElement.addEventListener("keydown", ShootFromKey, false);
        keyboard.domElement.addEventListener("keyup", ReloadFromKey, false);

    }

    function ShootFromKey(event)
    {
        var keyCode = event.keyCode;
        if(keyCode == 76 && canShoot) {
            makeBullet();
            canShoot = false;
        }
    }

    function ReloadFromKey(event)
    {
        var keyCode = event.keyCode;
        if(keyCode == 76)
        {
            canShoot = true;
        }
    }

    // Frame Update
    function draw() {

        if(userID == 0)
            return;


        c.clearRect(0,0,canvas.width, canvas.height);

        ship.targetVel.copyFrom(leftVector);


        ship.update();

        with(ship.pos) {
            if(x<0) x = canvas.width;
            else if(x>canvas.width) x = 0;
            if(y<0) y = canvas.height;
            else if (y>canvas.height) y = 0;
        }

        ship.draw();


        for (var i = 0; i < bullets.length; i++) {
            var bullet = bullets[i];
            if(!bullet.enabled) continue;
            bullet.update();
            bullet.draw(c);
            if(!bullet.enabled)
            {
                spareBullets.push(bullet);

            }
        }

        if(postTimeTracker > postRate) {
            postTimeTracker = 0;
            SendPositionData();
        }
        else {
            postTimeTracker += deltaTime;

            if(leftVector.x != 0 || leftVector.y != 0)
            {
                //console.log("Move " + leftVector.x + "," + leftVector.y);
                inputBuffer.push([leftVector.x, leftVector.y]);
            }

        }

        if(touchable) {

            for(var i=0; i<touches.length; i++) {

                var touch = touches[i];

                if(touch.identifier == leftTouchID){
                    c.beginPath();
                    c.strokeStyle = "cyan";
                    c.lineWidth = 6;
                    c.arc(leftTouchStartPos.x, leftTouchStartPos.y, 40,0,Math.PI*2,true);
                    c.stroke();
                    c.beginPath();
                    c.strokeStyle = "cyan";
                    c.lineWidth = 2;
                    c.arc(leftTouchStartPos.x, leftTouchStartPos.y, 60,0,Math.PI*2,true);
                    c.stroke();
                    c.beginPath();
                    c.strokeStyle = "cyan";
                    c.arc(leftTouchPos.x, leftTouchPos.y, 40, 0,Math.PI*2, true);
                    c.stroke();
                    timeSinceLastAction = 0;
                } else {

                    c.beginPath();
                    c.fillStyle = "white";
                    c.fillText("touch id : "+touch.identifier+" x:"+touch.clientX+" y:"+touch.clientY, touch.clientX+30, touch.clientY-30);

                    c.beginPath();
                    c.strokeStyle = "red";
                    c.lineWidth = "6";
                    c.arc(touch.clientX, touch.clientY, 40, 0, Math.PI*2, true);
                    c.stroke();
                    timeSinceLastAction = 0;
                }
            }
        }
        else {
            if( keyboard.pressed("right") ){
                leftVector.x = 1;
            }
            if( keyboard.pressed("left") ){
                leftVector.x = -1;
            }
            if( keyboard.pressed("up") ){
                leftVector.y = -1;
            }
            if( keyboard.pressed("down") ){
                leftVector.y = 1;
            }

            // Resetting direction
            if( !keyboard.pressed("right") && !keyboard.pressed("left"))
                leftVector.x = 0;
            if( !keyboard.pressed("up") && !keyboard.pressed("down"))
                leftVector.y = 0;

            //c.fillStyle	 = "white";
            //c.fillText("mouse : "+mouseX+", "+mouseY, mouseX, mouseY);

        }
        //c.fillText("hello", 0,0);

    }

    function SendPositionData()
    {
        if(inputBuffer.length == 0)
            return;

        var xmlhttp = new XMLHttpRequest();
        if(xmlhttp != null) {
            var url = "PostScore.php?";
            var movementDataAsJSON = JSON.stringify(inputBuffer);
            console.log(movementDataAsJSON);
            inputBuffer = [];
            url += "id=" + userID + "&inputs=" + movementDataAsJSON;

            xmlhttp.open("GET", url, true);
            //xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
            xmlhttp.send( );
            xmlhttp.onreadystatechange=function()
            {
                if (xmlhttp.readyState==4 && xmlhttp.status==200)
                {
                    //console.log(xmlhttp.responseText);
                    xmlhttp = null;
                }
            }
        }
    }

    function GetUserID()
    {

        var xmlhttp = new XMLHttpRequest();
        if(xmlhttp != null) {
            var url = "UserHandling.php";

            xmlhttp.open("GET", url, true);
            xmlhttp.send( );
            xmlhttp.onreadystatechange=function()
            {
                if (xmlhttp.readyState==4 && xmlhttp.status==200)
                {
                    userID = xmlhttp.responseText;
                    xmlhttp = null;
                }
            }
        }
    }

    // equivalent to using action button
    function makeBullet() {


        inputBuffer.push([0,0]);
        var bullet;

        if(spareBullets.length>0) {

            bullet = spareBullets.pop();
            bullet.reset(ship.pos.x, ship.pos.y, ship.angle);

        } else {

            bullet = new Bullet(ship.pos.x, ship.pos.y, ship.angle);
            bullets.push(bullet);

        }

        bullet.vel.plusEq(ship.vel);
    }

    /*
     *	Touch event (e) properties :
     *	e.touches: 			Array of touch objects for every finger currently touching the screen
     *	e.targetTouches: 	Array of touch objects for every finger touching the screen that
     *						originally touched down on the DOM object the transmitted the event.
     *	e.changedTouches	Array of touch objects for touches that are changed for this event.
     *						I'm not sure if this would ever be a list of more than one, but would
     *						be bad to assume.
     *
     *	Touch objects :
     *
     *	identifier: An identifying number, unique to each touch event
     *	target: DOM object that broadcast the event
     *	clientX: X coordinate of touch relative to the viewport (excludes scroll offset)
     *	clientY: Y coordinate of touch relative to the viewport (excludes scroll offset)
     *	screenX: Relative to the screen
     *	screenY: Relative to the screen
     *	pageX: Relative to the full page (includes scrolling)
     *	pageY: Relative to the full page (includes scrolling)
     */

    function onTouchStart(e) {

        for(var i = 0; i<e.changedTouches.length; i++){
            var touch =e.changedTouches[i];
            //console.log(leftTouchID + " "
            if((leftTouchID<0) && (touch.clientX<halfWidth))
            {
                leftTouchID = touch.identifier;
                leftTouchStartPos.reset(touch.clientX, touch.clientY);
                leftTouchPos.copyFrom(leftTouchStartPos);
                leftVector.reset(0,0);
                continue;
            } else {

                makeBullet();

            }
        }
        touches = e.touches;
    }

    function onTouchMove(e) {
        // Prevent the browser from doing its default thing (scroll, zoom)
        e.preventDefault();

        for(var i = 0; i<e.changedTouches.length; i++){
            var touch =e.changedTouches[i];
            if(leftTouchID == touch.identifier)
            {
                leftTouchPos.reset(touch.clientX, touch.clientY);
                leftVector.copyFrom(leftTouchPos);
                leftVector.minusEq(leftTouchStartPos);
                break;
            }
        }

        touches = e.touches;

    }

    function onTouchEnd(e) {

        touches = e.touches;

        for(var i = 0; i<e.changedTouches.length; i++){
            var touch =e.changedTouches[i];
            if(leftTouchID == touch.identifier)
            {
                leftTouchID = -1;
                leftVector.reset(0,0);
                break;
            }
        }

    }

    function onMouseMove(event) {

        mouseX = event.offsetX;
        mouseY = event.offsetY;
    }


    function setupCanvas() {

        canvas = document.createElement( 'canvas' );
        c = canvas.getContext( '2d' );
        container = document.createElement( 'div' );
        container.className = "container";

        document.body.appendChild( container );
        container.appendChild(canvas);

        resetCanvas();

        c.strokeStyle = "#ffffff";
        c.lineWidth =2;
    }


</script>
</body>
</html>


