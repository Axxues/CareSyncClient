document.addEventListener('DOMContentLoaded', function () {

    // ==========================================
    // 1. CHART.JS INITIALIZATION
    // ==========================================

    // A. Main Dashboard Chart (Line Chart)
    const patientsChartElement = document.getElementById('patientsChart');
    if (patientsChartElement) {
        const ctx = patientsChartElement.getContext('2d');

        let gradient = ctx.createLinearGradient(0, 0, 0, 300);
        gradient.addColorStop(0, 'rgba(16, 185, 129, 0.4)'); // Emerald 500 at 40%
        gradient.addColorStop(1, 'rgba(16, 185, 129, 0.0)'); // Transparent at bottom

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
                datasets: [{
                    label: 'New Patients',
                    data: [12, 19, 15, 25, 22, 30, 28],
                    borderColor: '#10b981',
                    backgroundColor: gradient,
                    borderWidth: 3,
                    pointBackgroundColor: '#ffffff',
                    pointBorderColor: '#10b981',
                    pointBorderWidth: 2,
                    pointRadius: 4,
                    pointHoverRadius: 6,
                    tension: 0.4,
                    fill: true
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        backgroundColor: '#0f172a',
                        titleFont: { family: 'Plus Jakarta Sans', size: 13 },
                        bodyFont: { family: 'Plus Jakarta Sans', size: 14, weight: 'bold' },
                        padding: 12,
                        cornerRadius: 8,
                        displayColors: false
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { borderDash: [4, 4], color: '#f1f5f9', drawBorder: false },
                        ticks: { font: { family: 'Plus Jakarta Sans' }, color: '#64748b' }
                    },
                    x: {
                        grid: { display: false, drawBorder: false },
                        ticks: { font: { family: 'Plus Jakarta Sans' }, color: '#64748b' }
                    }
                },
                interaction: { intersect: false, mode: 'index' }
            }
        });
    }

    // B. Reports Module Chart (Bar Chart)
    const reportingChartElement = document.getElementById('reportingChart');
    if (reportingChartElement) {
        const ctx = reportingChartElement.getContext('2d');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Patients Registered',
                    data: [65, 82, 70, 95, 110, 88],
                    backgroundColor: '#10b981',
                    borderRadius: 8,
                    barThickness: 20
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    y: { beginAtZero: true, grid: { borderDash: [4, 4], color: '#f1f5f9' } },
                    x: { grid: { display: false } }
                }
            }
        });
    }

    // ==========================================
    // 2. FLATPICKR (DATE PICKER) INITIALIZATION
    // ==========================================
    if (typeof flatpickr !== 'undefined') {

        // A. Patient Date of Birth (Patient Module)
        if (document.getElementById('dob-picker')) {
            flatpickr("#dob-picker", {
                dateFormat: "Y-m-d",
                altInput: true,
                altFormat: "F j, Y", // e.g. "October 12, 1991"
                maxDate: "today",
                disableMobile: true
            });
        }

        // B. Date Filter (Consultations & Inventory Index)
        if (document.getElementById('filter-date-picker')) {
            flatpickr("#filter-date-picker", {
                dateFormat: "Y-m-d",
                disableMobile: true
            });
        }

        // C. Visit Date (Consultations Module)
        if (document.getElementById('visit-date-picker')) {
            flatpickr("#visit-date-picker", {
                dateFormat: "Y-m-d",
                altInput: true,
                altFormat: "F j, Y",
                defaultDate: "today",
                maxDate: "today",
                disableMobile: true
            });
        }

        // D. Dispense Date (Pharmacy Module)
        if (document.getElementById('dispense-date-picker')) {
            flatpickr("#dispense-date-picker", {
                dateFormat: "Y-m-d",
                altInput: true,
                altFormat: "F j, Y",
                defaultDate: "today",
                maxDate: "today",
                disableMobile: true
            });
        }

        // E. Expiry Date (Pharmacy Module - handles both Create and Edit seamlessly)
        if (document.getElementById('expiry-date-picker')) {
            flatpickr("#expiry-date-picker", {
                dateFormat: "Y-m-d",
                altInput: true,
                altFormat: "F j, Y",
                minDate: "today",
                disableMobile: true
            });
        }

    } else {
        if (document.querySelector('[id$="-picker"]')) {
            console.error("Flatpickr is not loaded! Check the link to flatpickr.min.js in your HTML head.");
        }
    }

});