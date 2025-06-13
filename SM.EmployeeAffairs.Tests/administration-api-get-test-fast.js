import http from 'k6/http';
import {check, sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true, // Skip TLS verification for local testing
    noConnectionReuse: false, // Corrected typo: 'noconnectionReuse' to 'noConnectionReuse'
    vus: 500, // Number of virtual users
    duration: '10s', // Duration of the test
};

const BASE_URL = 'https://localhost:7065/api/administrations';

export default function () {
    let response = http.get(BASE_URL);
    check(response, {
        'Get all administrations: status is 200': (r) => r.status === 200,
    });
    sleep(1); // Pause between iterations
}
