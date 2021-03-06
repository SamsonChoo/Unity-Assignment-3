--------------------------------------------------------------------
	Interaction Design and Virtual Reality - Assignment 3

  Team 14 : Raphaël Boulanger - Samson Choo Han Ye - Cyrille Fahmi
--------------------------------------------------------------------

The purpose of this ReadMe file is to explain how we implemented our program for this assignment.
Our team had to implement the walking-in-place technique to be able to get 5 cubes in a 3D scene
using Virtual Reality and HTC Vive.

///////////////
Introduction :
///////////////

To implement this method we used the given package for the assignment 3. We mostly worked on a
new .cs file that we created ourselves called Mouvements.cs to handle the walking-in-place 
motion. We also added a Cube GameObject in the Unity interface that contains as a child 
GameObject the user camera. This is because the camera by itself is not a moveable object and
hence had to be a child of another moveable GameObject instead. We did not change much else
because the rest of the scene and scripts gave us a good and working structure to build our 
program upon (i.e. it already had working collisions with the controllers, the creation of the 
cubes, etc.). We just commented what was inside the Update() function of the ControllerBehaviour
script to disable the raycast from the controllers and the fact that you can attract the cubes to
the player with pressing the controllers' buttons since we wanted the players to get the cubes 
only by touching them.

We implemented the walk in place through the up and down movement of the controller. We did this
instead of using the head mounted display or full body movement to prevent motion sickness that
the movement of head could induce. According to the official Unity VR documentation page, 
in-place movement in VR is generally discouraged, and if must be implemented, it should avoid 
head bobbing at all cost as it has been proven to cause nausea and motion sickness easily. This 
is due to the fact that the VR view gives off the impression that the user is moving when the 
user is actually not, which is similar to the effects of poisoning. As a result, the body will 
self induce nausea as a defense mechanism since the brain wrongly interpreted that the user is 
poisoned. This effect is known as Vection. However, locomotion by the movement of arms will 
minimise the effect of Vection as the rest of the body stays still. Should the user have no issue
with motion sickness, he may still mimic the full body movement motion for better immersion and 
still achieve the intended in-game motion.

//////////////////////////////////////////////////////////////
Implementation of the walking-in-place locomotion technique :
//////////////////////////////////////////////////////////////

In the Mouvements.cs file, we proceeded like so :

- We created two SteamVR_TrackedObject for the two controllers.
- We created a Camera object that represents the main camera.
- We initialized two floats at 0 that keep a trace of the position of the two controllers.

- In the Update() function, we implemented the movements troughout two if conditions :

- For the first one, we check if the movements made by the player are higher than a chosen
value (0.1 here) : to do so, we checked, for each controller, if the absolute value of the 
difference between the vertical current controller position (controller.tranform.position.y) and
the previously kept position of the controller (leftpos or rightpos) is higher than 0.1.

- If so, we then verify two if conditions : 
	- if the current vertical position is under 0 (meaning we are going under the ground), we 
	first translate the player position up by using the absolute value of its y position to
	put its y coordinate at 0 and keep the player above the ground.
	- then in the second if condition, if the player gets too high (above 1.1 for the y
	coordinate), we put the player back at a vertical position of 1.1 to prevent the player
	from starting to fly towards the sky of the scene.

- After checking those two conditions, we then wrote a line to make the player walk forward by
adding to the position of the camera a product of : a term to make the camera move forward, times
a deltaTime term that represents the interval of time between the instant and the last movement,
times a last acceleration term we chose. Here it is 15 because the movement of the player is 
above 0.01 so it is quite a fast movement, hence why we make the camera move faster.

- The second if condition in the Update() function is basically the same but this time we check
if the movement is between 0.01 and 0.001. If so, we use the same two if conditions than before
to keep the player on the ground and not under or too high. Then we implement the movement again
but with a lower acceleration term (of 5) to make the camera move but slower since the controller 
movement inputted by the player is not as strong as for the first if condition.

- After those two if conditions, we then update the leftpos and rightpos variables with the new
positions of the left and right controllers.

We then have two conditions dealing with the movement speed of the player : if it is above 0.01
the player walks fast, between 0.001 and 0.01 we walk normally, and under 0.001 we consider the
movement is too slow to make the player walk. To find those two values of 0.01 and 0.001 and the
corresponding acceleration terms of 15 and 5, we tested our application several times to see
what fitted best.

We obtained the values 0.1 and 0.001 by testing with various movement speed of the controller and
printing out the vertical displacement, ultimately arriving at the optimal displacement per 
update of 0.1 and 0.001.

By using transform.position in our Movement.cs script, it does get the position of the camera,
because in the Unity interface, we added the Movement.cs script to the Cube GameObject that has
the main camera has a child. We also associated the controllerLeft member of the script with the
left controller object (called Left in the interface, child of the Main Camera (head) object),
the rightController object of the script with the Right object (right controller gameObject) of
the interface and finally the mainCamera of the script with the MainCamera (head) object.

///////////////////////////////
The Tunneling Effect (Bonus) :
///////////////////////////////

We tried several techniques to implement the tunneling effect but none of them worked. Here is 
a brief description of them :

- Manually changing the field of view :
Since the tunneling technique consists of reducing the field of view of the camera while the
player walks to reduce motion sickness, we thought about using the fieldOfView variable that we 
can access through a Camera object to reduce the field of view in our Update() function of the
Mouvements.cs file, before increasing it again when the player stays idle. But that did not work
because Unity does not allow us to modify the fieldOfView field of the Camera object while the
application is running (to avoid people to mess up with it and producing motion sickness).

- Using a VRTK Tunneling Asset and VR Tunelling Pro Assest :
We tried to implement this asset that is supposed to produce tunneling by just adding it to our 
project. We could make it work in an empty project by moving the headseat forward but we could
not make it work in our project using the walking-in-place technique.

- Using the Vignette Unity Asset :
We also tried to use this asset that can produce a very similar effect (displaying a black border
around the view that the player sees), but somehow it was impossible for us to modify the 
parameters of the Vignette asset after associating it with our camera object. We tried to access
to the object via code to modify the members of the Vignette object but after creating the object
in the Movements.cs script (and including the appropriate libraries), we could not associate it
with an object in the interface like we did with the controllers and the camera (the field was 
missing).

- Putting a circle frame in front of the camera view :
We also tried to show in front of the camera a frame consisting of a transparent filled disk in
the center and black borders on the side, like explained in the video of the PDF showed in class
(so that we could move the frame forward or towards the camera to reduce or increase the field of 
view). We tried to display a shader in front of the camera to do so but again we could not make
it work.

------------------------
End of the ReadMe file.
------------------------