

export const authServiceRegister = async (data) => {
    const response = await fetch('https://localhost:32768/api/Account/Register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        console.error(response);
        throw new Error(response);
        
    }

    return response.json();
}

export const authServiceLogin = async (data) => {
    const response = await fetch('https://localhost:32768/api/Account/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        console.error(response);
        throw new Error(response);
    }

    const responseData = await response.json();

    // Save the token in local storage
    localStorage.setItem('token', responseData.token);
    localStorage.setItem('role', responseData.role);

    return responseData;
}
