
function SigninForm() {
    return (
        <>
        <div className='signin-form'>
            <div className='signin-form__header'>
            <h1>Sign in</h1>
            </div>
            <div className='signin-form__body'>
            <form>
                <div className='form-group'>
                <label htmlFor='Email'>Email</label>
                <input type='Email' id='Email' name='Email' />
                </div>
                <div className='form-group'>
                <label htmlFor='Password'>Password</label>
                <input type='Password' id='Password' name='Password' />
                </div>
                <button type='submit'>Sign in</button>
            </form>
            </div>
        </div>
        </>
    )
    }

    export default SigninForm
