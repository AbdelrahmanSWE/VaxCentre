export async function reserveVaccineAtCenter(centerId, vaccineId) {
    const authToken = localStorage.getItem('token');
    const response = await fetch(`https://localhost:32768/api/VaccineCentre/Reserve/${centerId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ token: authToken, VaccineId: vaccineId })
    });

    if (!response.ok) {
        throw new Error('Failed to reserve vaccine at center');
    }

    const data = await response.json();
    return data;
}

export async function reserveSecondDoseAtCenter(centerId, vaccineId) {
    const authToken = localStorage.getItem('token');
    const response = await fetch(`https://localhost:32768/api/VaccineCentre/ReserveSecondDose/${centerId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ token: authToken, VaccineId: vaccineId })
    });

    if (!response.ok) {
        throw new Error('Failed to reserve second dose at center');
    }

    const data = await response.json();
    return data;
}