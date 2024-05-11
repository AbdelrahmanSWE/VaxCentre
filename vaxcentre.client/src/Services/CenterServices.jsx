export const RegisterVaccineCentre = async (data) => {
    const response = await fetch('https://localhost:32770/api/Account/VaccineCentreRegister', {
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


export async function fetchCenters() {

    try {
        const response = await fetch('https://localhost:32770/api/VaccineCentre', {
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

export async function deleteCenter(centerId) {
    try {
        const authToken = localStorage.getItem('token');
        const response = await fetch(`https://localhost:32770/api/VaccineCentre/Delete/${centerId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ token: authToken })
        });

        if (!response.ok) {
            throw new Error('Failed to delete center');
        }

        const data = await response.json();
        return data; // Assuming the response contains the deleted center information
    } catch (error) {
        console.error('Error deleting center:', error);
    }
}