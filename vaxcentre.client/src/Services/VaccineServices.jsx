export const CreateVaccine = async (data) => { 
    const authToken = localStorage.getItem('token');
    const response = await fetch('https://localhost:32768/api/Vaccine/Create', {
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
        const response = await fetch(`https://localhost:32768/api/Vaccine/Delete/${vaccineId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ token: authToken })
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

export async function editVaccine(vaccineId, updatedData) {
    try {
        
        const authToken = localStorage.getItem('token');
        console.log(JSON.stringify({ ...updatedData, token: authToken }));
        const response = await fetch(`https://localhost:32768/api/Vaccine/Update/${vaccineId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ ...updatedData, token: authToken })
        });

        if (!response.ok) {
            throw new Error('Failed to edit vaccine');
        }

        const data = await response.json();
        return data; // Assuming the response contains the updated vaccine information
    } catch (error) {
        console.error('Error editing vaccine:', error);
        throw error;
    }
}

export async function fetchVaccines() {

    try {
        const response = await fetch('https://localhost:32768/api/Vaccine', {
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
