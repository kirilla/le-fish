fetch('/upload/[[attack_token]]', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    message: 'This is some arbitrary JSON data.',
    number: 42,
    items: ['one', 'two', 'three']
  })
})
.then(response => {
     if (response.status === 202) {
        return null;
    }
    if (!response.ok) throw new Error(`HTTP error status: ${response.status}`);
    return response.text();
})
.catch(error => console.error('Error:', error));
