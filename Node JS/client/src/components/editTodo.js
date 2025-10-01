import React, { Fragment, useState } from "react";

const EditTodo = ({ todo }) => {
   // console.log(todo);
   const [description, setDescription] = useState(todo.description);

   //edit description function
const updateDescription = async (e) => {
    e.preventDefault();

    try {
const body = {description};
const response = await fetch(`http://localhost:5000/todos/${todo.todo_id}`, {
    method: "PUT",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(body)
});

window.location = "/";

    } catch (error) {
        console.log(error.message);
    }
}

return (
<Fragment>
<button type="button" class="btn btn-warning" data-bs-toggle="modal" 
data-bs-target={`#id${todo.todo_id}`}>
  Edit
</button>

<div class="modal" id={`id${todo.todo_id}`}
onClick={() => setDescription(todo.description)}>
  <div class="modal-dialog">
    <div class="modal-content">

      <div class="modal-header">
        <h4 class="modal-title">Edit Todo</h4>
        <button type="button" className="close" data-bs-dismiss="modal" 
        onClick={() => setDescription(todo.description)}>
            &times;</button>
      </div>

      <div class="modal-body">
        <input type="text" className="form-control" value={description} onChange={e => 
        setDescription(e.target.value)}/>
      </div>

      <div className="modal-footer">
      <button type="button" className="btn btn-warning" data-dismiss="modal"
      onClick = {e => updateDescription(e)}
      >
        Edit</button>
        <button type="button" className="btn btn-danger" data-bs-dismiss="modal"
        onClick={() => setDescription(todo.description)}>
            Close</button>
      </div>

    </div>
  </div>
</div>
</Fragment>
)};

export default EditTodo;

