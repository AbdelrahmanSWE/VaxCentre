import { authServiceLogin } from '../../Services/AuthService.jsx';
import { useNavigate } from 'react-router-dom';
function SigninForm() {
    const navigate = useNavigate();
    const login = async (e) => {
        e.preventDefault();

        const data = {
            UserName: e.target.UserName.value,
            Password: e.target.Password.value
        }
        try {
            const response = await authServiceLogin(data);
            console.log(response);

            // Redirect the user based on their role
            switch (response.role) {
                case 'Admin':
                    navigate('/admin');
                    break;
                case 'Patient':
                    navigate('/patient');
                    break;
                case 'VaccineCentre':
                    navigate('/VaccineCentre');
                    break;
                default:
                    // Handle unknown roles or stay at the current page
                    break;
            }
        } catch (error) {
            console.error(error);
        }
    }
    return (
        <>
        <div className='signin-form'>
            <div className='signin-form__header'>
            <h1>Sign in</h1>
            </div>
                <div className='signin-form__body'>
            <form onSubmit={login}>
                <div className='form-group'>
                <label htmlFor='UserName'>UserName</label>
                <input type='text' id='UserName' name='UserName' />
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
