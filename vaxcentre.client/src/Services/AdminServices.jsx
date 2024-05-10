export const fetchUnacceptedUsers = async (authToken) => {
    try {
        const response = await fetch('https://localhost:32768/api/Patient/Unapproved', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}` // Include the token in the request headers
            }
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
