import React from 'react'
import './Login.css'
function Login() {
  return (
    <div className="login-container">
    <form action="" className='login-form'>
      <div className="login-container-header">
        <h2>Login</h2>
      </div>
      <div className="input-group ">
        <label htmlFor="">Email</label>
        <input type="text" />
      </div>
      <div className="input-group h-10">
        <label htmlFor="">Password</label>
        <input type="password" />
      </div>
      <div className="input-group">
        <input type="checkbox" />
        <label htmlFor="">Remember me</label>
      </div>
      <div className="input-group">
        <a href="#">Forgot password?</a>
      </div>
      <div className="input-group">
        <button type="submit">Login</button>
      </div>

    </form>
    </div>
  )
}

export default Login