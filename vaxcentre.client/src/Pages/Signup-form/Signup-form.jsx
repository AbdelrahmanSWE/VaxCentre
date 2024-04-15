import {authServiceRegister} from '/src/Services/AuthService.jsx';


function SignupForm() {
    const signup = async(e) => {
        e.preventDefault();
        
        const data ={
            UserName:e.target.UserName.value,
            Email:e.target.Email.value,
            FirstName:e.target.FirstName.value,
            LastName:e.target.LastName.value,
            SSID:e.target.SSID.value,
            PhoneNumber:e.target.PhoneNumber.value,
            Address:e.target.Address.value,
            Password:e.target.Password.value
        }
        console.log(data);
        try{
            const result = await authServiceRegister(data);
            console.log('Signup successful', result);
        } catch (error) {
            console.error('Signup failed', error);
        }
     
    }


    return (
        <>
        <div className='signup-form'>
            <div className='signup-form__header'>
            <h1>Sign up</h1>
            </div>
            <div className='signup-form__body'>
            <form onSubmit={signup}>
                <div className='form-group'>
                <label htmlFor='UserName'>Username</label>
                <input type='text' id='UserName' name='UserName' />
                </div>
                <div className='form-group'>
                <label htmlFor='Email'>Email</label>
                <input type='Email' id='Email' name='Email' />
                </div>  
                <div className='form-group'>
                <label htmlFor='FirstName'>First Name</label>
                <input type='text' id='FirstName' name='FirstName' />
                </div>
                <div className='form-group'>
                <label htmlFor='LasttName'>Last Name</label>
                <input type='text' id='LastName' name='LastName' />
                </div>
                <div className='form-group'>
                <label htmlFor='SSID'>SSID</label>
                <input type='text' id='SSID' name='SSID' />
                </div>
                <div className='form-group'>
                <label htmlFor='PhoneNumber'>Phone Number</label>
                <input type='text' id='PhoneNumber' name='PhoneNumber' />
                </div>
                <div className='form-group'>
                <label htmlFor='Address'>Address</label>
                <input type='text' id='Address' name='Address' />
                </div>
                <div className='form-group'>
                <label htmlFor='Password'>Password</label>
                <input type='Password' id='Password' name='Password' />
                </div>
                <button type='submit'>Sign up</button>
            </form>
            </div>
        </div>
        </>
    )
    }

    export default SignupForm