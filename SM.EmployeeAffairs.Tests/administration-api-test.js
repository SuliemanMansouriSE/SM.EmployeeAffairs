import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true, // Skip TLS verification for local testing
    noConnectionReuse: false, // Corrected typo: 'noconnectionReuse' to 'noConnectionReuse'
    stages: [
        { duration: '10s', target: 5 }, // Ramp-up to 5 users
        { duration: '10s', target: 25 }, // Spike to 25 users
        { duration: '30s', target: 100 }, // Stay at 100 users for 30 seconds
        { duration: '10s', target: 25 }, // Ramp-down to 25 users
        { duration: '10s', target: 0 }, // Ramp-down to 0 users
    ],
};

const BASE_URL = 'https://localhost:7065/api/administrations';

export default function () {
    // Test GET all administrations
    let getAllResponse = http.get(BASE_URL);
    check(getAllResponse, {
        'GET all administrations: status is 200': (r) => r.status === 200,
    });

    sleep(1); // Pause between iterations
}

// To run the test, use the following command:
// k6 run SM.EmployeeAffairs.Tests\administration-api-test.js
