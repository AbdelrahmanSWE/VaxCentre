export const fetchUnacceptedUsers = async () => {
    // Get the token from local storage
    const authToken = localStorage.getItem('token');

    try {
        console.log(authToken)
        const response = await fetch('https://localhost:32770/api/Patient/Unapproved', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ token:authToken }) // Include the token in the request body
        });

        if (!response.ok) {
            throw new Error('Failed to fetch patients');
        }

        const data = await response.json();
        return data; // Assuming the response contains an array of patients
    } catch (error) {
        console.error('Error fetching patients:', error);
        throw error;
    }
};

export const acceptPatient = async (id) => {
    // Get the token from local storage
    const authToken = localStorage.getItem('token');

    const response = await fetch(`https://localhost:32770/api/Account/Activate/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ token: authToken }) // Include the token in the request body
    });

    if (!response.ok) {
        throw new Error('Failed to accept patient');
    }

    const data = await response.json();
    return data; // Assuming the response contains the patient data
};


