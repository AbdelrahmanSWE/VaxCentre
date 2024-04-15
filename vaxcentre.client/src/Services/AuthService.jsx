




export const authService = async (data) => {
    const response = await fetch('https://localhost:32768/api/Account/Register', {
        mode: 'no-cors',
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