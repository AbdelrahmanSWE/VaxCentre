export const CreateVaccine = async (data) => { 
    const authToken = localStorage.getItem('token');
    const response = await fetch('https://localhost:32770/api/Vaccine/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ ...data, token: authToken })
    });

    if (!response.ok) {
        console.error(response);
        throw new Error(response);

    }

    return response.json();
}

export async function deleteVaccine(vaccineId) {
    try {
        const authToken = localStorage.getItem('token');
        const response = await fetch(`https://localhost:32770/api/Vaccine/Delete/${vaccineId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to delete vaccine');
        }

        const data = await response.json();
        return data; // Assuming the response contains the deleted vaccine information
    } catch (error) {
        console.error('Error deleting vaccine:', error);
    }
}

export async function fetchVaccines() {

    try {
        const response = await fetch('https://localhost:32770/api/Vaccine', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
            
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
}
