using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsAbstractClassesMembers
    {
        /*Abstract Modifier declares that it is missing imiplementation.  For example in the Draw method in class Shape, there is no body for Draw, so call it "abstract"
              You don't have to use abstract all the time.  You use abstract when you want to provide common behaviour while forcing a developer to follow your design.
                A. Abstract member cannot include implementation Ex:
                 public abstract void Draw(); //no body
                B. if a member is abstract, the container class must also be abstract.
                C. In a Derived clas you ust implement all abstract members in the base abstract class (you have to override all)
                D. Abstract classes cannot be instantiated.
                */

        public void runAbstractClassesMembers()
        {

           // var shape = new AbstractClassesMembersShape() //cannot instantiate shape because it is abstract.  It can only be used as the base calss

            var circle = new AbstractClassesMembersCircle();
            circle.Draw();

            var rectangle = new AbstractClassesMembersRectangle();
            rectangle.Draw();
        }
    }
}
